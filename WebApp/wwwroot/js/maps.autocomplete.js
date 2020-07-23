class MapsAutocomplete {

	constructor() {
		self = this
	}

	init($container) {
		this.container = $container
		this.propertyName = $container.data('property-name')

		const components = $container.data('address-components')
		this.addressComponents = components ? JSON.parse(components.replace(/'/g, '"')) : null
		const inputElement = this.container.find('.search-input')[0]
		// Init autocomplete input
		const autocomplete = new google.maps.places.Autocomplete(inputElement, {})
		// Set initial point
		let latLong = new google.maps.LatLng(-34.55223, -58.4487099)
		if (this.addressComponents && this.addressComponents.Latitude != 0 & this.addressComponents.Longitude != 0) {
			latLong = new google.maps.LatLng(this.addressComponents.Latitude, this.addressComponents.Longitude)
			this.initAddressComponents()
		} else {
			this.clearAddressComponents()
		}
		// Init map
		const mapElement = this.container.find('.map')[0]
		this.map = new google.maps.Map(mapElement, {
			zoom: 15,
			center: latLong
		})
		// Init marker with initial point
		this.marker = new google.maps.Marker({
			map: this.map,
			anchorPoint: new google.maps.Point(20, 20)
		})
		this.marker.setPosition(latLong)

		// Add listeners
		this.addEventListeners(autocomplete, inputElement)
		// Hide search input if it's disabled
		const isDisabled = $container.attr('disabled')
		if (isDisabled) {
			this.container.find(".input-group").addClass("d-none")
		}
	}

	addEventListeners(autocomplete, inputElement) {
		autocomplete.addListener('place_changed', function () {
			self.currentPlace = this.getPlace()
			if (self.currentPlace.geometry) {
				self.updateMapLocation(self.currentPlace.geometry.location)
			}
		})

		// Prevent submit when the user select an address by hitting enter
		google.maps.event.addDomListener(inputElement, 'keydown', function (event) {
			if (event.keyCode === 13) {
				event.preventDefault()
			}
		})

		const clearButton = this.container.find('.input-group-append')
		clearButton.on('click', function () {
			self.clearLocation()
			self.clearAddressComponents()
		})
	}

	clearAddressComponents() {
		const elementsToClear = ['street', 'streetNumber', 'locality', 'city', 'country', 'postalCode', 'latitude', 'longitude', 'code', 'id']
			.map(e => {
				return `#${this.propertyName}_${e[0].toUpperCase() + e.slice(1)}`
			})

		for (const element of elementsToClear) {
			this.container.find(element)
				.attr('disabled', true)
				.attr('readonly', true)
				.val(null)
		}

		this.container.find('.search-input').val('')
	}

	clearLocation() {
		let latLong = new google.maps.LatLng(-34.55223, -58.4487099)
		this.updateMapLocation(latLong)
	}

	updateMapLocation(location) {
		this.marker.setVisible(false)
		this.map.setCenter(location)
		this.marker.setPosition(location)
		this.marker.setVisible(true)
		this.map.setZoom(15)
		this.updateAddressComponents()
	}

	initAddressComponents() {
		this.updateBindedProperty('street', this.addressComponents.Street)
		this.updateBindedProperty('streetNumber', this.addressComponents.StreetNumber)
		this.updateBindedProperty('locality', this.addressComponents.Locality)
		this.updateBindedProperty('city', this.addressComponents.City)
		this.updateBindedProperty('country', this.addressComponents.Country)
		this.updateBindedProperty('postalCode', this.addressComponents.PostalCode)
		this.updateBindedProperty('latitude', this.addressComponents.Latitude)
		this.updateBindedProperty('longitude', this.addressComponents.Longitude)
		this.updateBindedProperty('code', this.addressComponents.Code)
	}

	updateAddressComponents() {
		const streetNumber = this.getAddressComponent('street_number'),
			street = this.getAddressComponent('route'),
			locality = this.getAddressComponent('sublocality_level_1') || this.getAddressComponent('administrative_area_level_2'),
			city = this.getAddressComponent('administrative_area_level_1'),
			country = this.getAddressComponent('country'),
			postalCode = this.getAddressComponent('postal_code')

		// Update Friendly Labels
		this.updateBindedProperty('street', street)
		this.updateBindedProperty('streetNumber', streetNumber)
		this.updateBindedProperty('locality', locality)
		this.updateBindedProperty('city', city)
		this.updateBindedProperty('country', country)
		this.updateBindedProperty('postalCode', postalCode)

		if (this.currentPlace) {
			const code = this.currentPlace['id']
			const lat = this.currentPlace.geometry.location.lat()
			const lng = this.currentPlace.geometry.location.lng()
			this.updateBindedProperty('latitude', lat)
			this.updateBindedProperty('longitude', lng)
			this.updateBindedProperty('code', code)
		}
	}

	getAddressComponent(key) {
		if (this.currentPlace != null) {
			const component = this.currentPlace['address_components'].filter(c => c.types.indexOf(key) > -1)[0]
			if (component) {
				return component['long_name']
			}
		}
		return null
	}

	updateBindedProperty(id, value) {
		const element = this.container.find(`#${this.propertyName}_${id[0].toUpperCase() +
			id.slice(1)}`)
		element.val(value || '')
		element.attr('disabled', false)
		element.prop("readonly", value)
	}
}