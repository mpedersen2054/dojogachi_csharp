// feed, play, work, sleep
var $feedBtn  = $('.feed-btn'),
    $playBtn  = $('.play-btn'),
    $workBtn  = $('.work-btn'),
    $sleepBtn = $('.sleep-btn')


function updateDachi(act, callback) {
    var url;
    if (act == 'feed') url = '/feed'
    else if (act == 'play') url = '/play'
    else if (act == 'work') url = '/work'
    else if (act == 'sleep') url = '/sleep'
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
    console.log('hello werld!!! playbtn')
})

$workBtn.on('click', function(e) {
    console.log('hello werld!!! workbtn')
})

$sleepBtn.on('click', function(e) {
    console.log('hello werld!!! sleepbtn')
})
