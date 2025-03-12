import { Text, StyleSheet } from 'react-native'
import React from 'react'
import useTranslation from '../../../hooks/useTranslations'

interface DescriptionProps {
  description: string
}

const Description: React.FC<DescriptionProps> = ({ description }) => {
  const t = useTranslation()

  return <Text style={styles.text}>{description ? description : t('video.description')}</Text>
}

const styles = StyleSheet.create({
  text: {
    color: 'white',
    fontSize: 15,
  },
})
export default Description
