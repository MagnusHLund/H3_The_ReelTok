import React from 'react'
import { View, StyleSheet } from 'react-native'

interface DividerProps {
  height?: number
  isGray?: boolean
  widthPercentage?: number
}

const Divider: React.FC<DividerProps> = ({
  height = 4,
  isGray = 'black',
  widthPercentage = 80,
}) => {
  return (
    <View
      style={[
        styles.divider,
        { height, backgroundColor: isGray ? '#D3D3D3' : '#000', width: `${widthPercentage}%` },
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
