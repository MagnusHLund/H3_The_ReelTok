import React from 'react'
import { View, StyleSheet, ColorValue } from 'react-native'

interface DividerProps {
  height?: number
  color?: ColorValue
  widthPercentage?: number
}

const Divider: React.FC<DividerProps> = ({ height = 4, color = 'black', widthPercentage = 80 }) => {
  return (
    <View
      style={[styles.divider, { height, backgroundColor: color, width: `${widthPercentage}%` }]}
    />
  )
}

const styles = StyleSheet.create({
  divider: {
    alignSelf: 'center',
  },
})

export default Divider
