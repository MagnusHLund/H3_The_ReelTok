import { useEffect } from 'react'
import { useGeoLocation } from '../hooks/useGeoLocation'
import { useAppSelector } from '../hooks/useAppSelector'
import { useAppDispatch } from '../hooks/useAppDispatch'
import { changeLanguageThunk } from '../redux/thunks/settingsThunks'

// INFO: How It Works
// When the app loads, AutoLanguageSelector runs.
// If the user already chose a language, it does nothing.
// If no language is selected, it checks the user's country.
// If in Denmark, it selects Danish (da_DK).
// Otherwise, it selects English (en_GB).
// If location access is denied, it defaults to English

const AutoLanguageSelector: React.FC = () => {
  const { location, country } = useGeoLocation()
  const language = useAppSelector((state) => state.settings.language)
  const dispatch = useAppDispatch()

  useEffect(() => {
    if (language && language.locale) return

    if (country) {
      const selectedLanguage = country === 'Denmark' ? 'da_DK' : 'en_GB'
      dispatch(
        changeLanguageThunk({
          label: selectedLanguage === 'da_DK' ? 'Danish' : 'English',
          value: selectedLanguage,
        })
      )
    } else {
      dispatch(changeLanguageThunk({ label: 'English', value: 'en_GB' }))
    }
  }, [country, language, dispatch])

  return null
}

export default AutoLanguageSelector
