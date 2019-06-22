var map;
var markers = [];
var infowindow;
function initMap() {
    infowindow = new google.maps.InfoWindow;
    geocoder = new google.maps.Geocoder();
    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: -34.397, lng: 150.644 },
        zoom: 10,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    });
    var myLatlng = new google.maps.LatLng(-25.363882, 131.044922);
    var marker = new google.maps.Marker({
        position: myLatlng,
        title: "Hello World!"
    });
    var re = /,/
    // Try HTML5 geolocation.
    var lat;
    var long;
    var l = document.getElementById("Lat").value.split(re);
    var lg = document.getElementById("Long").value.split(re);

    if (document.getElementById("Lat").value != 0) {
        if (document.getElementById("Lat").value.indexOf(',') != -1) {
            lat = l[0] + '.' + l[1];
            long = lg[0] + '.' + lg[1];
        }
        else { lat = +l; long = +lg }
        var mrkr = new google.maps.LatLng(lat, long);
        addMarker(mrkr);
        map.setCenter(mrkr);
    }
    else if (navigator.geolocation) {
        var infoWindow = new google.maps.InfoWindow({ map: map });
        navigator.geolocation.getCurrentPosition(function (position) {
            var pos = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };
            infoWindow.setPosition(pos);
            infoWindow.setContent('Location found.');
            map.setCenter(pos);
        }, function () {
            handleLocationError(true, infoWindow, map.getCenter());
        });
    } else {
        // Browser doesn't support Geolocation
        handleLocationError(false, infoWindow, map.getCenter());
    }
}
    // Adds a marker to the map and push to the array.

    function addMarker(location) {
        var marker = new google.maps.Marker({
            position: location,
            map: map
        });
        document.getElementById("Lat").value = location.lat();
        document.getElementById("Long").value = location.lng();
        markers.push(marker);
        geocodeLatLng(marker,location)
    }

    // Sets the map on all markers in the array.
    function setMapOnAll(map) {
        for (var i = 0; i < markers.length; i++) {
            markers[i].setMap(map);
        }
    }

    // Removes the markers from the map, but keeps them in the array.
    function clearMarkers() {
        setMapOnAll(null);
    }

    // Shows any markers currently in the array.
    function showMarkers() {
        setMapOnAll(map);
    }
    function geocodeLatLng(marker, latlng) {
        geocoder.geocode({ 'location': latlng }, function (results, status) {
            if (status === google.maps.GeocoderStatus.OK) {
                if (results[0]) {
                    infowindow.setContent(results[0].formatted_address);
                    infowindow.open(map, marker);
                } else {
                    window.alert('No results found');
                }
            } else {
                window.alert('Geocoder failed due to: ' + status);
            }
        });
    }
   
 

