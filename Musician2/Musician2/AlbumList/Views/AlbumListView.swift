//
//  AlbumListView.swift
//  Musician2
//
//  Created by Maksim Ivanov on 05.07.2026.
//

import SwiftUI
import SwiftData

struct AlbumListView: View {
    @State private var viewModel: AlbumListViewModel

    init(viewModel: AlbumListViewModel) {
        _viewModel = State(wrappedValue: viewModel)
    }

    var body: some View {
        NavigationStack {
            Group {
                if viewModel.isLoading {
                    ProgressView("Loading albums…")
                } else if let error = viewModel.error, viewModel.albums.isEmpty {
                    ContentUnavailableView(
                        "Failed to load albums",
                        systemImage: "exclamationmark.triangle",
                        description: Text(error.localizedDescription)
                    )
                } else if viewModel.albums.isEmpty {
                    ContentUnavailableView(
                        "No albums",
                        systemImage: "music.note.list",
                        description: Text("Pull to refresh or try again later.")
                    )
                } else {
                    listContent
                }
            }
            .navigationTitle("Albums")
            .refreshable {
                await viewModel.loadAlbums()
            }
            .task {
                await viewModel.loadAlbums()
            }
        }
    }

    @ViewBuilder
    private var listContent: some View {
        List(viewModel.albums, id: \.albumId) { album in
            AlbumRowView(album: album)
                .listRowInsets(EdgeInsets())
                .listRowSeparator(.hidden)
        }
        .listStyle(.plain)
    }
}

// MARK: - Album Row

private struct AlbumRowView: View {
    let album: Album

    var body: some View {
        let albumColor = Color(hex: album.albumMedianColor) ?? .clear
        let textColor = albumTextColor(for: albumColor)

        HStack(alignment: .top, spacing: 0) {
            // Первый дочерний элемент: AsyncImage с фиксированными размерами и отступами
            AsyncImage(url: URL(string: album.albumCover)) { image in
                image
                    .resizable()
                    .aspectRatio(contentMode: .fill)
            } placeholder: {
                placeholderImage
            }
            .frame(width: 96, height: 96)
            .padding(.top, 24)
            .padding(.leading, 24)

            // Второй дочерний элемент: Контейнер, занимающий всю оставшуюся область
            VStack(alignment: .leading, spacing: 0) {
                Text(album.albumName)
                    .font(.system(size: 17, weight: .bold))
                    .foregroundColor(textColor)
                    .padding(.top, 24)
                    .padding(.leading, 24)
                    .padding(.trailing, 24)

                Spacer() // Толкает второй текст к нижнему краю

                Text("Альбом, \(album.albumYear)")
                    .font(.system(size: 15, weight: .bold))
                    .foregroundColor(textColor)
                    .padding(.bottom, 24)
                    .padding(.leading, 24)
            }
            // Растягивает VStack на всю доступную ширину и высоту
            .frame(maxWidth: .infinity, maxHeight: .infinity, alignment: .leading)
        }
        // Задаем точные размеры для корневого HStack
        .frame(maxWidth: .infinity)
        .frame(height: 144)
        .background(albumColor)
    }

    private var placeholderImage: some View {
        RoundedRectangle(cornerRadius: 8)
            .fill(Color.gray.opacity(0.3))
            .overlay {
                Image(systemName: "music.note")
                    .foregroundStyle(.secondary)
            }
    }
}

func albumTextColor(for color: Color) -> Color {
    color.isBright ? Color(red: 0.15, green: 0.15, blue: 0.15) : .white
}

// MARK: - Album Detail

private struct AlbumDetailView: View {
    let album: Album

    var body: some View {
        List {
            Section("Details") {
                LabeledContent("Name", value: album.albumName)
                LabeledContent("Year", value: "\(album.albumYear)")
            }

            Section("Tracks") {
                ForEach(album.tracks, id: \.trackId) { track in
                    VStack(alignment: .leading, spacing: 4) {
                        Text(track.name)
                            .font(.body)
                        Text(track.duration)
                            .font(.caption)
                            .foregroundStyle(.secondary)
                    }
                }
            }
        }
        .navigationTitle(album.albumName)
    }
}

#Preview {
    let config = ModelConfiguration(isStoredInMemoryOnly: true)
    let container = try! ModelContainer(for: Album.self, configurations: config)
    let context = container.mainContext

    let track = Track(trackId: 1, name: "Test Song", url: "https://example.com", duration: "3:45")
    let album = Album(
        albumId: 1,
        albumName: "Test Album",
        albumYear: 2024,
        albumCover: "http://maksimn.github.io/elizarov/jpg/notebook.jpg",
        albumMedianColor: "#FF5733",
        tracks: [track]
    )
    track.album = album
    context.insert(album)

    let dataLoader = URLSessionNetworkDataLoader()
    let repository = AlbumRepository(dataLoader: dataLoader, modelContext: context)
    let viewModel = AlbumListViewModel(repository: repository)

    return AlbumListView(viewModel: viewModel)
        .modelContainer(container)
}
