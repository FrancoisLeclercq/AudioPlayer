var wavesurfer = WaveSurfer.create({
    container: '#wave',
    progressColor: '#2f97f3',
    waveColor: '#a436d8',
    barHeight: 0.7,
    cursorWidth: 0,
    barWidth: 1
});

//wavesurfer.load('https://ia902606.us.archive.org/35/items/shortpoetry_047_librivox/song_cjrg_teasdale_64kb.mp3');

wavesurfer.on('ready', function () {
    wavesurfer.play();
});

document.getElementById('pause').addEventListener('click', function () {
    if (wavesurfer.isPlaying()) {
        wavesurfer.pause();
        document.querySelector('#pause > *').remove();
        $('#pause').append('<span class="far fa-play-circle"></span>');
    } else {
        wavesurfer.play();
        document.querySelector('#pause > *').remove();
        $('#pause').append('<span class="far fa-pause-circle"></span>');
    }
});

document.getElementById('mute').addEventListener('click', function () {
    if (wavesurfer.getMute()) {
        wavesurfer.setMute(false);
        document.querySelector('#mute > *').remove();
        $('#mute').append('<span class="fas fa-volume-up"></span>');
    } else {
        wavesurfer.setMute(true);
        document.querySelector('#mute > *').remove();
        $('#mute').append('<span class="fas fa-volume-mute"></span>');
    }
});

document.getElementById('close').addEventListener('click', function () {
    wavesurfer.destroy(wavesurfer.getDuration());
});

document.getElementById('previous').addEventListener('click', function () {
    wavesurfer.skipBackward(wavesurfer.getDuration());
});

document.getElementById('skip').addEventListener('click', function () {
    wavesurfer.skipForward(wavesurfer.getDuration());
});
