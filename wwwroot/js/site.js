// feed, play, work, sleep
var $feedBtn  = $('.feed-btn'),
    $playBtn  = $('.play-btn'),
    $workBtn  = $('.work-btn'),
    $sleepBtn = $('.sleep-btn')


function updateDachi(act, callback) {
    var url;
    if (act == 'feed') url = '/api/feed'
    else if (act == 'play') url = '/api/play'
    else if (act == 'work') url = '/api/work'
    else if (act == 'sleep') url = '/api/sleep'
    else callback('Something went wrong', null)

    $.post(url, function(data) {
        console.log(data, url)
    })
}


$feedBtn.on('click', function(e) {
    updateDachi('feed', function(err, data) {
        if (err) console.log(err)
        console.log('inside cb')
    })
})

$playBtn.on('click', function(e) {
    updateDachi('play', function(err, data) {
        if (err) console.log(err)
        console.log('inside cb')
    })
})

$workBtn.on('click', function(e) {
    updateDachi('work', function(err, data) {
        if (err) console.log(err)
        console.log('inside cb')
    })
})

$sleepBtn.on('click', function(e) {
    updateDachi('sleep', function(err, data) {
        if (err) console.log(err)
        console.log('inside cb')
    })
})
