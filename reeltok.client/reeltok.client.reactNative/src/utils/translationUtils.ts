import { LocaleCode } from '../redux/slices/settingsSlice'
import { readAsStringAsync } from 'expo-file-system'
import { parseString } from 'xml2js'
import { Asset } from 'expo-asset'

interface Translations {
  [key: string]: Translations | string
}

const translations: { [lang: string]: Translations } = {}

const loadXMLFile = async (assetUri: string): Promise<string> => {
  const asset = Asset.fromModule(assetUri)
  await asset.downloadAsync()
  return await readAsStringAsync(asset.localUri!)
}

const parseXML = (xml: string): Translations => {
  let result: Translations = {}
  const parserOptions = {
    trim: true,
    normalize: true,
    explicitRoot: false,
    explicitArray: false,
  }
  parseString(xml, parserOptions, (err, parsedResult) => {
    if (err) {
      console.error(`Error parsing XML: ${err.message}`)
      return
    }
    result = parsedResult
  })
  return result
}

const loadTranslations = async () => {
  try {
    const da_DK_Content = await loadXMLFile(require('./../../assets/translations/da_DK.xml'))
    const en_GB_Content = await loadXMLFile(require('./../../assets/translations/en_GB.xml'))

    translations['da_DK'] = parseXML(da_DK_Content)
    translations['en_GB'] = parseXML(en_GB_Content)
  } catch (error) {
    console.error('Error loading translations:', error)
  }
}

loadTranslations()

export const getTranslation = (lang: LocaleCode, key: string): string => {
  if (!translations[lang]) {
    console.error(`No translations found for language: ${lang}`)
    return key // Fallback to key if translation is not found
  }

  const keys = key.split('.')
  let result: Translations | string = translations[lang]

  for (const k of keys) {
    result = (result as Translations)[k]
    if (!result) {
      console.error(`Translation not found for key: ${key}`)
      return key // Fallback to key if translation is not found
    }
  }

  return result as string
}
