		document.addEventListener('DOMContentLoaded', function () {
            var calendarEl = document.getElementById('calendar');
            var calendar = new FullCalendar.Calendar(calendarEl, {
                initialView: 'dayGridMonth',
                nowIndicator: true,
                headerToolbar: {
                    left: 'prev,next today',
                    center: 'title', // 'addEventButton',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek'
                },
                navLinks: true, 
                editable: true,
                selectable: true,
                selectMirror: true,
                droppable: true,
                dayMaxEvents: true,
                weekNumbers: true,

                events: '/Home/LoadEvents', //ajax

                eventClick: function(info) {
                    info.jsEvent.preventDefault(); // don't let the browser navigate

                    var resp = GetDetails(info.event.id);
                    ShowEventPopupValues(resp.responseJSON);

                    if (info.event.url) { // open in another tab
                        window.open(info.event.url);
                    }
                },

                dateClick: function (date, allDay, jsEvent, view) {
                    var start_date = moment(date.dateStr).format('YYYY-MM-DDTHH:MM');
                    ShowEventPopup(start_date);
                },

                eventDidMount: function (info) {
                    $(info.el).tooltip({ //bootstrap tooltip
                        title: info.event.extendedProps.description,
                    });

                },
                eventWillUnmount: function (info) {
                    $(info.el).tooltip('dispose');
                }
            });
            calendar.render();
        });
		
		$('#btnClose').click(function () {
            $('#schedule').modal('hide');
        });

        //onclick does not honor html5 validation
        $("#btnSave").click(function (e) { 
            if ($("#Title")[0].checkValidity() && $("#Start")[0].checkValidity() && 
                $("#End")[0].checkValidity() && $("#Description")[0].checkValidity() &&
                $("#BackgroundColor")[0].checkValidity() && $("#Url")[0].checkValidity()) {
                e.preventDefault();Add();
            }
            else {
                return 0;
            }
        });

        $("#btnEdit").click(function () {
            if ($("#Title")[0].checkValidity() && $("#Start")[0].checkValidity() &&
                $("#End")[0].checkValidity() && $("#Description")[0].checkValidity() &&
                $("#BackgroundColor")[0].checkValidity() && $("#Url")[0].checkValidity()) {
                Update();
            }
            else {
                return 0;
            }
        });

        function ClearPopupFormValues() {
            //clear forms after closing
            $('#Title').val("");
            $('#Description').val("");
            $('#Start').val("");
            $('#End').val("");
            $('#Url').val("");
            $('#AllDay').prop('checked', false);
            $('input:radio[name=BackgroundColor]').each(function () { $(this).prop('checked', false); });
        }

        function ShowEventPopup(sendDate) {
            ClearPopupFormValues();
            $('#Start').val(sendDate);
            $('#End').val(sendDate);  //value="2020-03-13T18:22" seconds from number of month
            $('#schedule').modal('show');
            $('#btnEdit').hide();
            $('#Completed').hide();
            $('#completed').hide();
            $('#btnSave').show();
            $('#Title').focus();
        }
        
        function ShowEventPopupValues(res) {
            ClearPopupFormValues();
            //$('#Id').val("5");
            $('#Id').val(res.id);
            $('#Title').val(res.title);
            $('#Description').val(res.description);
            $('#Start').val(res.start);
            $('#End').val(res.end);
            $('#Url').val(res.url);
            $('#AllDay').prop('checked', res.allDay);
            $('input:radio[name=BackgroundColor]').each(function () {
                if($(this).val() == res.backgroundColor) {
                    $(this).prop('checked', true);
                } else {
                    $(this).prop('checked', false);
                }
            });
            $('#schedule').modal('show');
            $('#btnEdit').show();
            $('#btnSave').hide();
            $('#Title').focus();
            $('#Completed').show();
            $('#completed').show();
        }

        function GetDetails(id) {
            return $.ajax({
                dataType: 'json',
                type: "GET",
                url: "/Home/GetEvent",
                data: { id: id },
                async: false,
                success: function (res) {
                    if (res == null)
                    {
                        alert("Event is not exists!");
                    }
                },
                false: function (errormessage) {
                    alert(errormessage.responseText);
                }
            });
        }

        function Add() {
            var eventdto =
            {
                title: $('#Title').val(),
                description: $('#Description').val(),
                start: $('#Start').val(),
                end: $('#End').val(),
                allDay: $('#AllDay').is(':checked'),
                url: $('#Url').val(),
                backgroundColor: $('input[name="BackgroundColor"]:checked').val()
            };
            
            $.ajax({
                url: "/Home/CreateEvent",
                data: JSON.stringify(eventdto),
                type: "POST",
                async: false, //запрос делается асинхронно и когда приходит ответ на запрос - сама функция уже закончила свое выполнение
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    $('#schedule').modal('hide');
                    alert(result);
                    $('#calendar').fullCalendar('refetchEvents'); //it works on delete and update
                },
                error: function (errormessage) {
                    //var data = errormessage.responseJSON;
                    //var jsonData = JSON.parse(JSON.stringify(data));
                    //console.log(jsonData);
                    console.log(errormessage.responseJSON.Start[0]);
                    //for (n1 in data) {
                    //    console.log(n1);
                    //}
                    alert(errormessage.responseText);
                }
            });
        }

        function Delete() {
            var eventId = $('#Id').val();
            $.ajax({
                url: "/Home/DeleteEvent",
                data: JSON.stringify(eventId),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) { //why it is not working from the first time? if i waiting for success
                    $('#schedule').modal('hide'); 
                    alert(result);
                    $('#calendar').fullCalendar('refetchEvents');
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            });
        }

        function Update() {
            var eventdto =
            {
                id: $('#Id').val(),
                title: $('#Title').val(),
                description: $('#Description').val(),
                start: $('#Start').val(),
                end: $('#End').val(),
                allDay: $('#AllDay').is(':checked'),
                url: $('#Url').val(),
                backgroundColor: $('input[name="BackgroundColor"]:checked').val()
            };

            $.ajax({
                url: "/Home/UpdateEvent",
                data: JSON.stringify(eventdto),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    $('#schedule').modal('hide');
                    alert(result);
                    $('#calendar').fullCalendar('refetchEvents'); //it works on delete
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            });
        }