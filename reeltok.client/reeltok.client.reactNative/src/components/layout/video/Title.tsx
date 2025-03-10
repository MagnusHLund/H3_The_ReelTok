import { Text, StyleSheet } from 'react-native'
import React from 'react'

interface TitleProps {
  title: string
}

const Title: React.FC<TitleProps> = ({ title }) => {
  return <Text style={styles.text}>{title ? 'Ingen title' : ''}</Text>
}
const styles = StyleSheet.create({
  text: {
    color: 'white',
    fontSize: 15,
  },
})
export default Title
