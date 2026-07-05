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
            NavigationLink {
                AlbumDetailView(album: album)
            } label: {
                AlbumRowView(album: album)
            }
        }
    }
}

// MARK: - Album Row

private struct AlbumRowView: View {
    let album: Album

    var body: some View {
        HStack(spacing: 12) {
            AsyncImage(url: URL(string: album.albumCover)) { phase in
                switch phase {
                case .success(let image):
                    image
                        .resizable()
                        .scaledToFill()
                case .failure:
                    placeholderImage
                case .empty:
                    ProgressView()
                @unknown default:
                    placeholderImage
                }
            }
            .frame(width: 60, height: 60)
            .clipShape(RoundedRectangle(cornerRadius: 8))

            VStack(alignment: .leading, spacing: 4) {
                Text(album.albumName)
                    .font(.headline)
                    .lineLimit(1)

                Text("\(album.albumYear)")
                    .font(.subheadline)
                    .foregroundStyle(.secondary)

                Text("\(album.tracks.count) tracks")
                    .font(.caption)
                    .foregroundStyle(.secondary)
            }
        }
        .padding(.vertical, 4)
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
        albumCover: "https://example.com/cover.jpg",
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