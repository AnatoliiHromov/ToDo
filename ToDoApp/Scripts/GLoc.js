var geocoder;
var map;
var markers = [];
var bool = false;
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
    var infoWindow = new google.maps.InfoWindow({ map: map });
    var marker = new google.maps.Marker({
        position: myLatlng,
        title: "Hello World!"
    });
    // Try HTML5 geolocation.
    if (navigator.geolocation) {
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

  
    map.addListener('click', function (e) {
    markListener(e.latLng);
  });
    // обработчик нажатия на маркер объекта
  google.maps.event.addListener(marker, 'click', function () {
      infowindow.open(map, marker);
  });
}

function markListener(location) {
    if (bool) {
        deleteMarkers();
        addMarker(location);
    }
    else {
        addMarker(location);
        bool = true;
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
        geocodeLatLng(marker,location);        
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

    // Deletes all markers in the array by removing references to them.
    function deleteMarkers() {
        document.getElementById("address").value = null;
        document.getElementById("Lat").value = null;
        document.getElementById("Long").value = null;
        clearMarkers();
        markers = [];
    }
   
    function handleLocationError(browserHasGeolocation, infoWindow, pos) {
        infoWindow.setPosition(pos);
        infoWindow.setContent(browserHasGeolocation ?
                              'Error: The Geolocation service failed.' :
                              'Error: Your browser doesn\'t support geolocation.');
    }

    function codeAddress() {
        var address = document.getElementById("address").value;
        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                map.setCenter(results[0].geometry.location);
                markListener(results[0].geometry.location);
            } else {
                alert("Geocode was not successful for the following reason: " + status);
            }
        });
    }
    function geocodeLatLng(marker,latlng) {
        geocoder.geocode({ 'location': latlng }, function (results, status) {
            if (status === google.maps.GeocoderStatus.OK) {
                if (results[0]) {
                    infowindow.setContent(results[0].formatted_address);
                    infowindow.open(map, marker);
                    document.getElementById("address").value=results[0].formatted_address;
                } else {
                    window.alert('No results found');
                }
            } else {
                window.alert('Geocoder failed due to: ' + status);
            }
        });
}
