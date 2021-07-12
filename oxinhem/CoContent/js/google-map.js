
var google;

function init() {
    // Basic options for a simple Google Map
    // For more options see: https://developers.google.com/maps/documentation/javascript/reference#MapOptions
    // var myLatlng = new google.maps.LatLng(40.71751, -73.990922);
    var myLatlng = new google.maps.LatLng(56.850880, 14.823638);
    var uluru = { lat: 56.850880, lng: 14.823638 };
    // 39.399872
    // -8.224454
    
    var mapOptions = {
        // How zoomed in you want the map to start at (always required)
        zoom: 17,

        // The latitude and longitude to center the map (always required)
        center: uluru,
        
        // How you would like to style the map. 
        scrollwheel: false,
        styles: [
            {
                "featureType": "administrative.country",
                "elementType": "geometry",
                "stylers": [
                    {
                        "visibility": "simplified"
                    },
                    {
                        "hue": "#ff0000"
                    }
                ]
            }
        ]
    };

    

    // Get the HTML DOM element that will contain your map 
    // We are using a div with id="map" seen below in the <body>
    var mapElement = document.getElementById('map');

    // Create the Google Map using out element and options defined above
    var map = new google.maps.Map(mapElement, mapOptions);
    var contentString = '<p><strong>OXINHEM</strong><br>Oxinhem</p>';
    var addresses = ['New York'];
    
    for (var x = 0; x < addresses.length; x++) {
        $.getJSON('http://maps.googleapis.com/maps/api/geocode/json?address='+addresses[x]+'&sensor=false', null, function (data) {
            //var p = data.results[0].geometry.location;
            //var latlng = new google.maps.LatLng(myLatlng.lat, myLatlng.lng);
            var marker = new google.maps.Marker({
                position: uluru,
                map: map
            });
            var infowindow = new google.maps.InfoWindow({
                content: contentString
            });

            google.maps.event.addListener(marker, 'click', function () {
                infowindow.open(map, marker);
            });

        });
    }
    
}
google.maps.event.addDomListener(window, 'load', init);
