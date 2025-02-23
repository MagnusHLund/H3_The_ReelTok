import { Text, View, StyleSheet } from 'react-native'
import React from 'react'

interface SettingProps {
  title?: string
  children: React.ReactNode
}

const Setting: React.FC<SettingProps> = ({ title = '', children }) => {
  return (
    <View>
      {title != '' && <Text style={styles.container}>{title}</Text>}
      <View style={styles.children}>{children}</View>
    </View>
  )
}

const styles = StyleSheet.create({
  container: { fontSize: 20, fontWeight: '600' },
  children: { height: 50 },
})

export default Setting
