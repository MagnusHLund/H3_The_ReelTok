import { View, Text, StyleSheet } from 'react-native'
import React from 'react'

interface UsernameProps {
  username: string
}

const Username: React.FC<UsernameProps> = ({ username }) => {
  return (
    <View>
      <Text style={styles.username}>{username}</Text>
    </View>
  )
}

const styles = StyleSheet.create({
  username: {
    fontSize: 16,
    fontWeight: 'bold',
  },
})
export default Username
