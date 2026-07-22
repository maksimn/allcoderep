//
//  AVAudioPlayerService.swift
//  Musician2
//
//  Created by Maksim Ivanov on 22.07.2026.
//

import AVFoundation

final class AVAudioPlayerService: AudioPlayerService {

    private var audioPlayer: AVAudioPlayer?

    var currentTime: TimeInterval {
        audioPlayer?.currentTime ?? 0
    }

    var duration: TimeInterval {
        audioPlayer?.duration ?? 0
    }

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
