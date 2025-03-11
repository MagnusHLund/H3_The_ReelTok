import { Text, StyleSheet } from 'react-native'
import React from 'react'
import useTranslation from '../../../hooks/useTranslations'

interface TitleProps {
  title: string
}
const Title: React.FC<TitleProps> = ({ title }) => {
  const t = useTranslation()

  return <Text style={styles.text}>{title ? title : t('video.title')}</Text>
}
const styles = StyleSheet.create({
  text: {
    color: 'white',
    fontSize: 15,
  },
})
export default Title
