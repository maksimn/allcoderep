//
//  AudioPlayerView.swift
//  Musician2
//
//  Created by Maksim Ivanov on 12.07.2026.
//

import SwiftData
import SwiftUI

struct AudioPlayerView: View {

    let track: Track

    var body: some View {
        HStack(spacing: 0) {
            Button(action: {

            }) {
                Image("icon-play")
                    .resizable()
                    .frame(width: 44, height: 44)
            }
            .padding(.leading, 10)
            .padding(.top, 2)

            VStack(alignment: .leading, spacing: 0) {
                HStack(spacing: 0) {
                    Text(track.name)
                        .font(.system(size: 16, weight: .bold))
                        .foregroundColor(.white)
                        .lineLimit(1)
                        .padding(.leading, 10)

                    Spacer()

                    Text(track.duration)
                        .font(Font.custom("Helvetica Bold", size: 16))
                        .foregroundColor(.white.opacity(0.8))
                        .padding(.trailing, 20)
                }
                .padding(.top, 4)

                Rectangle()
                    .fill(.white)
                    .frame(width: .infinity, height: 2)
                    .padding(.leading, 10)
                    .padding(.trailing, 56)
                    .padding(.top, 8)
            }
            .padding(.top, -6)
            .padding(.leading, 6)
        }
        .frame(maxWidth: .infinity)
        .frame(height: 76)
        .background(Color(red: 0.15, green: 0.15, blue: 0.15))
    }
}

#Preview {
    let track = Track(
        trackId: 1,
        name: "Test Song",
        url: "https://example.com",
        duration: "3:45"
    )

    AudioPlayerView(track: track)
        .modelContainer(for: [Album.self, Track.self], inMemory: true)
}
