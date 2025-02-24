import { LocaleCode } from '../redux/slices/settingsSlice'
import { readAsStringAsync } from 'expo-file-system'
import { Platform } from 'react-native'
import { parseString } from 'xml2js'
import { Asset } from 'expo-asset'

interface Translations {
  [key: string]: Translations | string
}

const translations: { [lang: string]: Translations } = {}

const loadXMLFile = async (assetUri: string): Promise<string> => {
  if (Platform.OS === 'web') {
    const response = await fetch(assetUri)
    if (!response.ok) {
      throw new Error(`Failed to load asset: ${assetUri}`)
    }
    return await response.text()
  } else {
    const asset = Asset.fromModule(assetUri)
    await asset.downloadAsync()
    return await readAsStringAsync(asset.localUri!)
  }
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
  const webTranslationsDirectory = '/translations'
  const appTranslationsDirectory = './../../public/translations'

  try {
    const da_DK_Content = await loadXMLFile(
      Platform.OS === 'web'
        ? `${webTranslationsDirectory}/da_DK.xml`
        : require(`${appTranslationsDirectory}/da_DK.xml`)
    )
    const en_GB_Content = await loadXMLFile(
      Platform.OS === 'web'
        ? `${webTranslationsDirectory}/en_GB.xml`
        : require(`${appTranslationsDirectory}/en_GB.xml`)
    )

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
    return key
  }

  const keys = key.split('.')
  let result: Translations | string = translations[lang]

  for (const k of keys) {
    result = (result as Translations)[k]
    if (!result) {
      console.error(`Translation not found for key: ${key}`)
      return key
    }
  }

  return result as string
}
