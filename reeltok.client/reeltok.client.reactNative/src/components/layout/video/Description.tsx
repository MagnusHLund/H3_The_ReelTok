import { Text, StyleSheet } from 'react-native'
import React from 'react'

interface DescriptionProps {
  description: string
}

const Description: React.FC<DescriptionProps> = ({ description }) => {
  return <Text style={styles.text}>{description ? 'Ingen beskrivelse' : ''}</Text>
}

const styles = StyleSheet.create({
  text: {
    color: 'white',
    fontSize: 15,
  },
})
export default Description
