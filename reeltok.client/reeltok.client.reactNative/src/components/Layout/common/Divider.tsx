import React from 'react'
import { View, StyleSheet } from 'react-native'

const Divider = ({ height = 4, color = 'black' }) => {
  return <View style={[styles.divider, { height, backgroundColor: color }]} />
}

const styles = StyleSheet.create({
  divider: {
    width: '80%', // Ensures it spans the full width
  },
})

export default Divider
