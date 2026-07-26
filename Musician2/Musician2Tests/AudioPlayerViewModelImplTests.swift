//
//  AudioPlayerViewModelImplTests.swift
//  Musician2Tests
//
//  Created by Maksim Ivanov on 26.07.2026.
//

import Testing
@testable import Musician2
import Foundation

// MARK: - Local Fakes (namespaced to avoid conflicts)

final class APVMI_NetworkDataLoaderFake: NetworkDataLoader {
    enum FakeError: Error { case failed }

    var result: Result<Data, Error>

    init(result: Result<Data, Error>) {
        self.result = result
    }

    func download(_ url: URL) async throws -> Data {
        switch result {
        case .success(let data):
            return data
        case .failure(let error):
            throw error
        }
    }
}

final class APVMI_AudioPlayerAPIFake: AudioPlayerAPI {
    weak var delegate: AudioPlayerDelegate?

    var didInitialize = false
    var isPlaying = false

    var currentTime: TimeInterval = 0
    var duration: TimeInterval = 0

    func initialize(with data: Data) throws {
        didInitialize = true
    }

    func play() {
        isPlaying = true
    }

    func pause() {
        isPlaying = false
    }

    func finish() {
        delegate?.didFinishPlaying()
    }
}

final class APVMI_TimerAPIFake: TimerAPI {
    private var block: (@MainActor () -> Void)?

    func start(block: @escaping @MainActor () -> Void) {
        self.block = block
    }

    func stop() {
        self.block = nil
    }

    @MainActor func tick() {
        guard let block else { return }
        block()
    }
}

// MARK: - Tests

@Suite("AudioPlayerViewModelImpl tests")
@MainActor
struct AudioPlayerViewModelImplTests {

    // Helper
    private func makeSUT(
        track: Track = Track(trackId: 1, name: "Test Track", url: "https://example.com/audio.mp3", duration: "1:08"),
        loaderResult: Result<Data, Error> = .success(Data())
    ) -> (vm: AudioPlayerViewModelImpl, loader: APVMI_NetworkDataLoaderFake, audio: APVMI_AudioPlayerAPIFake, timer: APVMI_TimerAPIFake) {
        let loader = APVMI_NetworkDataLoaderFake(result: loaderResult)
        let audio = APVMI_AudioPlayerAPIFake()
        let timer = APVMI_TimerAPIFake()
        let vm = AudioPlayerViewModelImpl(track: track, dataLoader: loader, audioPlayerAPI: audio, timerAPI: timer)
        return (vm, loader, audio, timer)
    }

    @Test("loadTrack sets state to .loaded on success")
    func load_success_sets_loaded() async {
        let (vm, _, _, _) = makeSUT()
        await vm.loadTrack()
        #expect(vm.state == .loaded)
    }

    @Test("loadTrack sets state to .error on invalid URL")
    func load_invalidURL_sets_error() async {
        let badTrack = Track(trackId: 2, name: "Bad", url: "not a url", duration: "0:00")
        let (vm, _, _, _) = makeSUT(track: badTrack)
        await vm.loadTrack()
        #expect(vm.state == .error)
    }

    @Test("loadTrack sets state to .error on download failure")
    func load_failure_sets_error() async {
        let failing: Result<Data, Error> = .failure(APVMI_NetworkDataLoaderFake.FakeError.failed)
        let (vm, _, _, _) = makeSUT(loaderResult: failing)
        await vm.loadTrack()
        #expect(vm.state == .error)
    }

    @Test("play from .loaded transitions to .playing")
    func play_from_loaded_transitions_to_playing() async {
        let (vm, _, audio, _) = makeSUT()
        audio.duration = 60
        await vm.loadTrack()
        vm.play()
        #expect(vm.state == .playing)
    }

    @Test("play while .playing transitions to .paused")
    func play_while_playing_transitions_to_paused() async {
        let (vm, _, audio, _) = makeSUT()
        audio.duration = 60
        await vm.loadTrack()
        vm.play()
        vm.play()
        #expect(vm.state == .paused)
    }

    @Test("progress updates after timer tick")
    func progress_updates_after_timer_tick() async {
        let (vm, _, audio, timer) = makeSUT()
        audio.duration = 60
        audio.currentTime = 15
        await vm.loadTrack()
        vm.play()
        timer.tick()
        #expect(vm.progress == 0.25)
    }

    @Test("timeDisplay shows track.duration when inactive")
    func time_display_inactive_shows_track_duration() async {
        let track = Track(trackId: 3, name: "T", url: "https://example.com/a.mp3", duration: "9:59")
        let (vm, _, _, _) = makeSUT(track: track)
        #expect(vm.timeDisplay == track.duration)
    }

    @Test("timeDisplay shows formatted current time when active")
    func time_display_active_shows_formatted_time() async {
        let (vm, _, audio, timer) = makeSUT()
        audio.duration = 60
        audio.currentTime = 15
        await vm.loadTrack()
        vm.play()
        timer.tick()
        #expect(vm.timeDisplay == "0:15")
    }

    @Test("progressValue is 1.0 when inactive")
    func progress_value_inactive_is_one() async {
        let (vm, _, _, _) = makeSUT()
        #expect(vm.progressValue == 1.0)
    }

    @Test("didFinishPlaying resets state to .initial")
    func did_finish_playing_resets_state() async {
        let (vm, _, audio, _) = makeSUT()
        await vm.loadTrack()
        vm.play()
        audio.finish()
        #expect(vm.state == .initial)
    }
}
