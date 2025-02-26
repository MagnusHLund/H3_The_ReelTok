import { useEffect, useState } from 'react'
import * as Location from 'expo-location'

export const useGeoLocation = () => {
  const [location, setLocation] = useState<Location.LocationObjectCoords | null>(null)
  const [country, setCountry] = useState<string>('')

  useEffect(() => {
    ;(async () => {
      let { status } = await Location.requestForegroundPermissionsAsync()
      if (status !== 'granted') {
        console.log('Permission to access location was denied')
        return
      }

      // Get user's location
      let locationData = await Location.getCurrentPositionAsync({})
      setLocation(locationData.coords)

      // Reverse geocode to get country
      let reverseGeocode = await Location.reverseGeocodeAsync({
        latitude: locationData.coords.latitude,
        longitude: locationData.coords.longitude,
      })

      if (reverseGeocode.length > 0) {
        setCountry(reverseGeocode[0].country || '') // Get country name
      }
    })()
  }, [])

  return { location, country }
}
