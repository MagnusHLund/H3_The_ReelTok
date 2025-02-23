import { useAppSelector } from './useAppSelector'
import { getTranslation } from './../utils/translationUtils'

const useTranslation = () => {
  const languageState = useAppSelector((state) => state.settings.language)
  const currentLanguageLocale = languageState.locale

  const t = (key: string): string => {
    return getTranslation(currentLanguageLocale, key)
  }

  return t
}

export default useTranslation
