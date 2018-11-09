var data = JSON.stringify({
  "notification": {
    "title": "Travel Tips - Digify",
    "body": "Weather for your Travel",
    "click_action": "OPEN_ACTIVITY_1",
    "icon": "ic_notification"
  },
  "priority": "high",
  "data": {
    "picture": "https://pbs.twimg.com/media/DijOdcTUwAAgBiq.jpg",
    "contents": "https://web.facebook.com/Travel-Tips-Digify-713185002389816/?modal=admin_todo_tour"
  },
  "to": "<TO>"
});

var xhr = new XMLHttpRequest();
xhr.withCredentials = true;

xhr.addEventListener("readystatechange", function () {
  if (this.readyState === 4) {
    console.log(this.responseText);
  }
});

xhr.open("POST", "https://fcm.googleapis.com/fcm/send");
xhr.setRequestHeader("content-type", "application/json");
xhr.setRequestHeader("authorization", "key=<KEY>");
xhr.setRequestHeader("cache-control", "no-cache");

xhr.send(data);