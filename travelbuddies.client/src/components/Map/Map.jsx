import React, { Component } from 'react';
import GoogleMapReact from 'google-map-react';

class MapContainer extends Component {
  state = {
    origin: null,
    destination: null
  };

  componentDidMount() {
    this.geocodeAddress();
  }

  componentDidUpdate(prevProps) {
    if (prevProps.originCity !== this.props.originCity || prevProps.destinationCity !== this.props.destinationCity) {
      this.geocodeAddress();
    }
  }

  geocodeAddress = () => {
    const { originCity, destinationCity } = this.props;
    const geocoder = new window.google.maps.Geocoder();

    geocoder.geocode({ address: originCity }, (results, status) => {
      if (status === 'OK' && results[0]) {
        const { lat, lng } = results[0].geometry.location;
        this.setState({ origin: { lat, lng } });
      } else {
        console.error('Geocode was not successful for the following reason:', status);
      }
    });

    geocoder.geocode({ address: destinationCity }, (results, status) => {
      if (status === 'OK' && results[0]) {
        const { lat, lng } = results[0].geometry.location;
        this.setState({ destination: { lat, lng } });
      } else {
        console.error('Geocode was not successful for the following reason:', status);
      }
    });
  };

  render() {
    const { origin, destination } = this.state;

    if (!origin || !destination) {
      return null; // Wait for geocoding to complete
    }

    return (
      <div style={{ height: '400px', width: '100%' }}>
        <GoogleMapReact
          bootstrapURLKeys={{ key: 'AIzaSyC8rHskVCyhZmNS4ivdSv3IXtV6gBA84EQ' }}
          defaultCenter={{
            lat: (origin.lat + destination.lat) / 2,
            lng: (origin.lng + destination.lng) / 2
          }}
          defaultZoom={7}
        >
          <Marker
            lat={origin.lat}
            lng={origin.lng}
            text="Origin"
          />
          <Marker
            lat={destination.lat}
            lng={destination.lng}
            text="Destination"
          />
        </GoogleMapReact>
      </div>
    );
  }
}

const Marker = ({ text }) => <div style={{ color: 'red' }}>{text}</div>;

export default MapContainer;
