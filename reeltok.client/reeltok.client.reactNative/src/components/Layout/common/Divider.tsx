import React from 'react'
import { View, StyleSheet } from 'react-native'

interface DividerProps {
  height?: number
  grayColor?: boolean
  widthPercentage?: number
}

const Divider: React.FC<DividerProps> = ({
  height = 4,
  grayColor = 'black',
  widthPercentage = 80,
}) => {
  return (
    <View
      style={[
        styles.divider,
        { height, backgroundColor: grayColor ? '#D3D3D3' : '#000', width: `${widthPercentage}%` },
      ]}
    />
  )
}

const styles = StyleSheet.create({
  divider: {
    alignSelf: 'center',
  },
})

export default Divider
