import { LinearGradient } from 'expo-linear-gradient'
import { StyleSheet } from 'react-native'
import React, { ReactNode } from 'react'

interface GradientProps {
  colors: [string, string, ...string[]]
  children?: ReactNode
}

const Gradient: React.FC<GradientProps> = ({ colors, children }) => {
  return (
    <LinearGradient colors={colors} style={styles.gradient}>
      {children}
    </LinearGradient>
  )
}

const styles = StyleSheet.create({
  gradient: {
    width: '100%',
    height: '100%',
  },
})

export default Gradient
