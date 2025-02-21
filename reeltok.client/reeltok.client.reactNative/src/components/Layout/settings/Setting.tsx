import React from 'react'
import { Text, View } from 'react-native'

interface SettingProps {
  title?: string
  children: React.ReactNode
}

const Setting: React.FC<SettingProps> = ({ title = '', children }) => {
  return (
    <View>
      {title != '' && <Text style={{ fontSize: 20, fontWeight: '600' }}>{title}</Text>}
      <View style={{ height: 50 }}>{children}</View>
    </View>
  )
}

export default Setting
