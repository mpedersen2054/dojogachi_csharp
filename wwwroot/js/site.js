// feed, play, work, sleep
var $feedBtn = $('.feed-btn'),
    $playBtn = $('.play-btn'),
    $workBtn = $('.work-btn'),
    $sleepBtn = $('.sleep-btn'),
    $fullness = $('.fullness'),
    $happiness = $('.happiness'),
    $energy = $('.energy'),
    $meals = $('.meals'),
    $message = $('.message')


function updateDachi(act, callback) {
    var url;
    if (act == 'feed') url = '/api/feed'
    else if (act == 'play') url = '/api/play'
    else if (act == 'work') url = '/api/work'
    else if (act == 'sleep') url = '/api/sleep'
    else callback('Something went wrong', null)

    $.post(url, function(data) {
        // console.log(data, url)
        callback(null, data)
    })
}


$feedBtn.on('click', function(e) {
    updateDachi('feed', function(err, data) {
        if (err || data.error == "true") return
        $message.html(data.msg)
        $fullness.html(data.newFullness)
        $meals.html(data.newMeals)
        console.log('updated dom for feed!')
    })
})

$playBtn.on('click', function(e) {
    updateDachi('play', function(err, data) {
        if (err || data.error == "true") return
        $message.html(data.msg)
        $happiness.html(data.newHappiness)
        $energy.html(data.newEnergy)
    })
})

$workBtn.on('click', function(e) {
    updateDachi('work', function(err, data) {
        if (err || data.error == "true") return
        $message.html(data.msg)
        $energy.html(data.newEnergy)
        $meals.html(data.newMeals)
    })
})

$sleepBtn.on('click', function(e) {
    updateDachi('sleep', function(err, data) {
        if (err) console.log(err)
        console.log('inside cb')
    })
})
