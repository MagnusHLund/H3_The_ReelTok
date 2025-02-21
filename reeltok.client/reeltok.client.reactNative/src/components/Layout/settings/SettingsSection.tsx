import React, { ReactNode } from 'react'
import { StyleSheet, Text, View } from 'react-native'
import Divider from '../common/Divider'

interface SettingsSectionProps {
  title?: string
  displayDivider?: boolean
  children: ReactNode
}

const SettingsSection: React.FC<SettingsSectionProps> = ({
  title,
  displayDivider = true,
  children,
}) => {
  return (
    <View style={styles.container}>
      {displayDivider && <Divider />}
      <View>
        {title != '' && <Text style={styles.title}>{title}</Text>}
        {children}
      </View>
    </View>
  )
}

const styles = StyleSheet.create({
  container: { gap: 10, alignItems: 'center' },
  title: { fontSize: 25, fontWeight: '900' },
})

export default SettingsSection
