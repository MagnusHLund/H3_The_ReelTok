// Looks at language in the global state and gets the correct file to look in, for translations.
// Take a look at this: https://github.com/MagnusHLund/Resume/blob/main/Web%20client/src/Hooks/useTranslation.ts
// It does what this has to do, but for xml instead.

import { useSelector } from 'react-redux'
import { RootState } from './../redux/store'
import { getTranslation } from './../utils/translationUtils'

const useTranslation = () => {
  const languageState = useSelector((state: RootState) => state.language)
  const currentLanguage = languageState.languages[languageState.current]

  const t = (key: string): string => {
    return getTranslation(currentLanguage, key)
  }

  return { t }
}

export default useTranslation
