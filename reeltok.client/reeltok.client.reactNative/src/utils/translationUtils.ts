import { parseStringPromise } from 'xml2js'
import da_DK from './../../assets/translations/da_DK.xml'
import en_GB from './../../assets/translations/en_GB.xml'

interface Translations {
  [key: string]: any // TODO: change to string
}

const translations: { [lang: string]: Translations } = {}

const parseXML = async (xml: string): Promise<Translations> => {
  const result = await parseStringPromise(xml)
  return result.translations
}

const loadTranslations = async () => {
  translations['da_DK'] = await parseXML(da_DK)
  translations['en_GB'] = await parseXML(en_GB)
}

;(async () => {
  await loadTranslations()
})()

export const getTranslation = (lang: string, key: string): string => {
  const keys = key.split('.')
  let result: any = translations[lang]

  for (const k of keys) {
    result = result?.[k]
    if (!result) {
      return key // Fallback to key if translation is not found
    }
  }

  return result || key
}
