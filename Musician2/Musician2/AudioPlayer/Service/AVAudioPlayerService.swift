//
//  AVAudioPlayerService.swift
//  Musician2
//
//  Created by Maksim Ivanov on 22.07.2026.
//

import AVFoundation

final class AVAudioPlayerService: AudioPlayerService {

    private var audioPlayer: AVAudioPlayer?

    func initialize(with data: Data) throws {
        audioPlayer = try AVAudioPlayer(data: data)
    }

    func play() {
        audioPlayer?.play()
    }

    func pause() {
        audioPlayer?.pause()
    }
}
