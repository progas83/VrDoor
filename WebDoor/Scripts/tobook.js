jQuery(document).ready(function ($) {

    var response;   //Last response with list of available time and free places
    var currentReserveStart; //What time-list element is current for start
    var currentReserveEnd; //What time-list element is current for end
    var currentReserveTime = []; //Array of time, what we have to reserve
    var currentMaxPlaces; //How much places are available is current time range
    var currentPlaces; //How much places are choose now
    var reserveDate; //Date which choose now to reserve
    var reserveDateNext; //Next reserve date 
    var reserveDatePrev; //Prev reserve date 
    var globalCurrentDate; //Date for switch month on calendar

    /**
     * Get days in months
     * @param {any} month
     * @param {any} year
     */
    function daysInMonth(month, year) {
        return new Date(year, month, 0).getDate();
    }

    /**
     * Generate the calendar
     * @param {any} currentDate
     */
    function generateCalendar (currentDate = new Date()) {

        globalCurrentDate = currentDate;
        let out = '';
        let monthName = '';

        switch (currentDate.getMonth()) {
            case 0:
                monthName='<div class="calendar__head">Январь</div>';
                break;
            case 1:
                monthName='<div class="calendar__head">Февраль</div>';
                break;
            case 2:
                monthName='<div class="calendar__head">Март</div>';
                break;
            case 3:
                monthName='<div class="calendar__head">Апрель</div>';
                break;
            case 4:
                monthName='<div class="calendar__head">Май</div>';
                break;
            case 5:
                monthName='<div class="calendar__head">Июнь</div>';
                break;
            case 6:
                monthName='<div class="calendar__head">Июль</div>';
                break;
            case 7:
                monthName='<div class="calendar__head">Август</div>';
                break;
            case 8:
                monthName='<div class="calendar__head">Сентябрь</div>';
                break;
            case 9:
                monthName='<div class="calendar__head">Октябрь</div>';
                break;
            case 10:
                monthName='<div class="calendar__head">Ноябрь</div>';
                break;
            case 11:
                monthName='<div class="calendar__head">Декабрь</div>';
                break;
        }

       $('#month-name').html(monthName);

        let requestData = {
            'month':currentDate.getMonth()+1, // +1 needs because js count from 0 and server count from 1
            'year':currentDate.getFullYear()
        };

        $.ajax({
            type: "GET",
            url: "/api/monthReservations",
            data: requestData,
            success: function (ajaxResponse) {

                // /api/monthReservations?year=2018&month=11
                // let response = JSON.parse('["20","10","18","36","20","10","18","36","20","10","18","36","20","10","18","36","1","10","18","36","20","10","18","36","20","10","18","36","20","10","18"]');
                
                let response = JSON.parse(ajaxResponse);

                //Make head of calendar
                out += '<div class="weekday">Пн</div><div class="weekday">Вт</div><div class="weekday">Ср</div><div class="weekday">Чт</div><div class="weekday">Пт</div><div class="weekday">Сб</div><div class="weekday">Вс</div>';

                //Make days
                let daysCount = daysInMonth(currentDate.getMonth() + 1, currentDate.getFullYear());

                let firstDay = new Date(currentDate.getFullYear(), currentDate.getMonth(), 1).getDay();

                if (firstDay === 0) {
                    firstDay = 7; //fix for Sunday
                }
                //Create empty items before first day of month
                while (firstDay > 1) {
                    firstDay--;
                    out += '<div class="empty-cell"></div>';
                }

                //Create days
                let day = 1;
                while (daysCount >= day) {

                    firstDay = new Date(currentDate.getFullYear(), currentDate.getMonth(), day, 23, 59, 59);

                    out += '<div class="cell';
                    if (firstDay > new Date()) {
                        out += ' active"  data-toggle="modal" href="#reserveModal"';
                    }
                    out += '"><span class="calendar__day">' + day + '</span><span class="calendar__places">';

                    if (firstDay < new Date()) {
                        out += '- мест';
                    } else {
                        out += '<a class="calendar__reserve">' + response[(day - 1)] + ' мест</a>';
                    }
                    out += '</span></div>';

                    day++;
                }

                $('#calendar').html(out);
            }
        });

    }

    /**
     * Fill data to reservation modal
     * @param {any} day
     */
    function showReserveModal(day) {

        //Make request to server
        let requestData = {
            'day': day.getDate(),
            'month': day.getMonth() + 1, // +1 needs because js count from 0 and server count from 1
            'year': day.getFullYear()
        };
        reserveDate = requestData; //update day to reserve. This is gloabl variable

        reserveDateNext = new Date(day.getFullYear(), day.getMonth(), (day.getDate() + 1), 12, 0, 0, 0);
        reserveDatePrev = new Date(day.getFullYear(), day.getMonth(), (day.getDate() - 1), 12, 0, 0, 0);

        $.ajax({
            type: "GET",
            url: "/api/Reservations",
            data: requestData,
            success: function (ajaxResponse) {

                //TODO delete this
                //ajaxResponse = '{"DateReservation":"2018-12-26T00:00:00","FreeHelmets":[{"StartHour":14,"FreeHelmets":2},{"StartHour":15,"FreeHelmets":1},{"StartHour":16,"FreeHelmets":4},{"StartHour":17,"FreeHelmets":4},{"StartHour":18,"FreeHelmets":0},{"StartHour":19,"FreeHelmets":3},{"StartHour":20,"FreeHelmets":2},{"StartHour":21,"FreeHelmets":4},{"StartHour":22,"FreeHelmets":4}]}';

                $('#reserve-day').html(('0' + String(day.getDate())).slice(-2) + '.' + ('0' + String(parseInt(day.getMonth()) + 1)).slice(-2) + '.' + day.getFullYear());

                //Reset form
                $('#reserve-button').css('display', 'inline-block');
                $('.reserve__message').html('');
                $('.reserve__buttons-wrapper').html('');

                response = JSON.parse(ajaxResponse);

                if (response['FreeHelmets'].length > 0) {
                    
                    response['time-list'] = Array();
                    for (let i = 0; i < response['FreeHelmets'].length; i++) {
                        response['time-list'][i] = {
                            'time-start': response['FreeHelmets'][i]['StartHour'] + ':00',
                            'time-end': parseInt(response['FreeHelmets'][i]['StartHour']) + 1 + ':00',
                            'free-places': response['FreeHelmets'][i]['FreeHelmets']
                        };
                    }
                }

                let out = '';

                if (response['time-list'].length > 0) {

                    $('#begin-time').html(response['time-list'][0]['time-start']);
                    $('#end-time').html(response['time-list'][0]['time-end']);
                    $('#men-quantity').html(response['time-list'][0]['free-places']);
                    currentReserveStart = currentReserveEnd = 0;
                    currentMaxPlaces = currentPlaces = parseInt(response['time-list'][0]['free-places']);

                    for (let i = 0; i < response['time-list'].length; i++) {
                        out += '<div id="reserve-id-' + i + '" class="col-xs-6 col-sm-4 reserve__list-item';
                        if (response['time-list'][i]['free-places'] < 1) {
                            out += ' busy ';
                        }
                        out += '"><input type="checkbox" id="reserve-cb-' + i + '" name="reserve-cb-' + i + '" value="' + i + '">' +
                            '<label for="reserve-cb-' + i + '"><span class="reserve__time">' + response['time-list'][i]['time-start'] + '-' + response['time-list'][i]['time-end'] +
                            '</span><span class="reserve__places"> - мест: ' + response['time-list'][i]['free-places'] + '</span></label ></div>';
                    }
                } else {
                    out += 'Извините, на этот день все места заняты';
                }

                $('#time-list').html(out);
            }

        });

    }

    /**
     * Build request to backend
     * @param {any} name
     * @param {any} tel
     */
    function buildRequest(name, tel) {
        return {
            'time-start': response['time-list'][currentReserveStart]['time-start'],
            'time-end': response['time-list'][currentReserveEnd]['time-end'],
            'places': currentPlaces,
            'name': name,
            'phone': tel,
            'date': reserveDate
        };
    }

    /* ======================================================= */
    /* ================= Listen to events ==================== */
    /* ======================================================= */

    // Click to arrow left on calendar (change month to previous)
    $(document).on('click', '#calendar-left', function (e) {
        e.preventDefault();
        globalCurrentDate.setMonth(globalCurrentDate.getMonth() - 1);
        generateCalendar(globalCurrentDate);
    });

    // Click to arrow right on calendar (change month to next)
    $(document).on('click', '#calendar-right', function (e) {
        e.preventDefault();
        globalCurrentDate.setMonth(globalCurrentDate.getMonth() + 1);
        generateCalendar(globalCurrentDate);
    });

    // Click to day on calendar
    $(document).on('click', '.cell', function (e) {
        let day = new Date(globalCurrentDate.getFullYear(), globalCurrentDate.getMonth(), $(this).find('.calendar__day').text(), 12, 0, 0, 0);
        showReserveModal(day);
    });

    // Click left on reservation modal (change day to prev)
    $(document).on('click', '#reserve-day-prev', function (e) {
        showReserveModal(reserveDatePrev);
    });

    // Click right on reservation modal (change day to next)
    $(document).on('click', '#reserve-day-next', function (e) {
        showReserveModal(reserveDateNext);
    });

    // Click to time checkbox
    $(document).on('change', '.reserve__list-item input', function(e) {
      
        let list = $('.reserve__list-item input:checked');

        console.log(list);

        currentReserveTime = [];
        let minHelmets;
        console.log(list.length);
        for (let i = 0; i < list.length; i++) {
            console.log($(list[i]).val());
            
            currentReserveTime[i] = {
                'time-start' : response['time-list'][parseInt($(list[i]).val())]['time-start'],
                'time-end' : response['time-list'][parseInt($(list[i]).val())]['time-end'],
            }

            if(i == 0) {
                minHelmets = parseInt(response['time-list'][parseInt($(list[i]).val())]['free-places']);
            } else {
                minHelmets = (minHelmets > parseInt(response['time-list'][parseInt($(list[i]).val())]['free-places'])) ? parseInt(response['time-list'][parseInt($(list[i]).val())]['free-places']) : minHelmets;
            }
            
        } 

        let out = '<h4>Выберите количество шлемов:</h4><fieldset id="group2">';

        for (let i = 0; i < minHelmets; i++) {
            let j = i + 1;
            out += '<input type="radio" id="helmets-' + j + '" name="helmets" value="' + j + '"';
            out += ( i == 0 ) ? 'checked' : '';
            out += '/><label for="helmets-' + j + '">' + j + '</label>';
        }

        out += '</fieldset>';

        $('.reserve__buttons-wrapper').html(out);

        console.log(currentReserveTime);

        console.log('min' + minHelmets);

        if(this.checked) {
            console.log('check');
        } else {
            console.log('uncheck');
        }
    });

    // Submit reserve
    $(document).on('click', '#reserve-button', function (e) {
        
        if(currentPlaces < 1) {
            $('.reserve__message').html('<span class="alert-danger">Количество мест должно быть больше 0</span>');
            e.preventDefault();
        }
        if($('#name').val().length < 3) {
            $('.reserve__message').html('<span class="alert-danger">Заполните поле имя</span>');
            e.preventDefault();
        }
        if($('#tel').val().length !== 17) {
            $('.reserve__message').html('<span class="alert-danger">Заполните поле телефон</span>');
            e.preventDefault();
        }

        let data = JSON.stringify(buildRequest($('#name').val(), $('#tel').val()));
        
        $.ajax({
            type: "POST",
            url: "/api/MakeReservation",
            data: data,
            success: function (ajaxResponse) {
                if (ajaxResponse == true) {
                    $('#reserve-button').css('display', 'none');
                    $('.reserve__message').html('<span class="alert-success">Забронировано</span>');
                } else {
                    $('.reserve__message').html('<span class="alert-danger">' + ajaxResponse + '</span>');
                }
                
            },
            error: function () {
                $('.reserve__message').html('<span class="alert-danger">Ошибка связи. Попробуйте еще раз или свяжитесь с нами по телефону</span>');
            }
        });

    });

    //set mask for phone number
    $("#tel").mask("+38(050)000-00-00");

    //init calendar
    generateCalendar();

});