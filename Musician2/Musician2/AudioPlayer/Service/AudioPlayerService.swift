//
//  AudioPlayerService.swift
//  Musician2
//
//  Created by Maksim Ivanov on 22.07.2026.
//

import Foundation

protocol AudioPlayerService {

    func initialize(with data: Data) throws

    func play()

    func pause()

    var currentTime: TimeInterval { get }

    var duration: TimeInterval { get }
}

protocol AudioPlayerTimingService {

    var currentTime: TimeInterval { get }

    var duration: TimeInterval { get }
}
