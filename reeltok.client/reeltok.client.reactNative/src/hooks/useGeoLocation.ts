import { useEffect, useState } from 'react'
import * as Location from 'expo-location'
import { useAppDispatch } from '../hooks/useAppDispatch'
import { useAppSelector } from '../hooks/useAppSelector'
import { changeLanguageThunk } from '../redux/thunks/settingsThunks'

const useGeoLocationUtils = () => {
  const [location, setLocation] = useState<Location.LocationObjectCoords | null>(null)
  const [country, setCountry] = useState<string>('')
  const dispatch = useAppDispatch()
  const language = useAppSelector((state) => state.settings.language)

  useEffect(() => {
    ;(async () => {
      let { status } = await Location.requestForegroundPermissionsAsync()
      if (status !== 'granted') {
        console.log('Permission to access location was denied')
        return
      }

      let locationData = await Location.getCurrentPositionAsync({})
      setLocation(locationData.coords)

      let reverseGeocode = await Location.reverseGeocodeAsync({
        latitude: locationData.coords.latitude,
        longitude: locationData.coords.longitude,
      })

      if (reverseGeocode.length > 0) {
        setCountry(reverseGeocode[0].country || '')
      }
    })()
  }, [])

  useEffect(() => {
    if (!language || !language.locale) {
      const selectedLanguage = country === 'Denmark' ? 'da_DK' : 'en_GB'
      dispatch(
        changeLanguageThunk({
          label: selectedLanguage === 'da_DK' ? 'Danish' : 'English',
          value: selectedLanguage,
        })
      )
    }
  }, [country, language, dispatch])

  return { location, country }
}

export default useGeoLocationUtils
