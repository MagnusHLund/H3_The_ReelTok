import { Text, StyleSheet } from 'react-native'
import React from 'react'

interface UsernameProps {
  username: string
}

const Username: React.FC<UsernameProps> = ({ username }) => {
  return <Text style={styles.text}>{username ? 'intet brugernavn' : ''}</Text>
}

const styles = StyleSheet.create({
  text: {
    color: 'white',
    fontSize: 15,
    fontWeight: 'bold',
  },
})
export default Username
