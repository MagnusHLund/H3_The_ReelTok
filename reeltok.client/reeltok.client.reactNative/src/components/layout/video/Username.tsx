import { Text, StyleSheet } from 'react-native'
import React from 'react'
import useTranslation from '../../../hooks/useTranslations'

interface UsernameProps {
  username: string
}

const Username: React.FC<UsernameProps> = ({ username }) => {
  const t = useTranslation()

  return <Text style={styles.text}>{username ? username : t('video.username')}</Text>
}

const styles = StyleSheet.create({
  text: {
    color: 'white',
    fontSize: 15,
    fontWeight: 'bold',
  },
})
export default Username
