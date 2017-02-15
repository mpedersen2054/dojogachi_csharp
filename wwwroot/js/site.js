var $feedBtn = $('.feed-btn'),
    $playBtn = $('.play-btn'),
    $workBtn = $('.work-btn'),
    $sleepBtn = $('.sleep-btn'),
    $fullness = $('.fullness'),
    $happiness = $('.happiness'),
    $energy = $('.energy'),
    $meals = $('.meals'),
    $message = $('.message'),
    $dachiImg = $('.dachi-img')

// make request to server, api varies based
// on arg passed in ('feed', 'play', ...)
function updateDachi(act, callback) {
    var url;
    if (act == 'feed') url = '/api/feed'
    else if (act == 'play') url = '/api/play'
    else if (act == 'work') url = '/api/work'
    else if (act == 'sleep') url = '/api/sleep'
    else callback('Something went wrong', null)

    $.post(url, function(data) {
        $dachiImg.attr('src', data.imgUrl)
        callback(null, data)
    })
}

// organize handler functions
var handlers = {
    handleFeed: function(e) {
        updateDachi('feed', function(err, data) {
            var msg
            if (err) return // not the best error handling lol
            if (data.err == 'true') {
                msg = `${data.msg} <a href="/reset" class="btn playagn-btn btn-success">Play again?</a>`
            } else {
                msg = data.msg
            }
            $message.html(msg)
            $fullness.html(data.newFullness)
            $meals.html(data.newMeals)
        })
    },

    handlePlay: function(e) {
        updateDachi('play', function(err, data) {
            if (err) return
            var msg
            if (data.err == 'true') {
                msg = `${data.msg} <a href="/reset" class="btn playagn-btn btn-success">Play again?</a>`
            } else {
                msg = data.msg
            }
            $message.html(msg)
            $happiness.html(data.newHappiness)
            $energy.html(data.newEnergy)
        })
    },

    handleWork: function(e) {
        updateDachi('work', function(err, data) {
            if (err) return
            var msg
            if (data.err == 'true') {
                msg = `${data.msg} <a href="/reset" class="btn playagn-btn btn-success">Play again?</a>`
            } else {
                msg = data.msg
            }
            $message.html(msg)
            $meals.html(data.newMeals)
            $energy.html(data.newEnergy)
        })
    },

    handleSleep: function(e) {
        updateDachi('sleep', function(err, data) {
            if (err) return
            var msg
            if (data.err == 'true') {
                msg = `${data.msg} <a href="/reset" class="btn playagn-btn btn-success">Play again?</a>`
            } else {
                msg = data.msg
            }
            $message.html(msg)
            $fullness.html(data.newFullness)
            $happiness.html(data.newHappiness)
            $energy.html(data.newEnergy)
        })
    }
}

// attach handler functions
$feedBtn.on('click', handlers.handleFeed)
$playBtn.on('click', handlers.handlePlay)
$workBtn.on('click', handlers.handleWork)
$sleepBtn.on('click', handlers.handleSleep)