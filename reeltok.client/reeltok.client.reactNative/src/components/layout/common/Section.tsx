import { StyleSheet, Text, View } from 'react-native'
import React, { ReactNode } from 'react'
import Divider from '../common/Divider'

interface SectionProps {
  title?: string
  displayDivider?: boolean
  children: ReactNode
}

const Section: React.FC<SectionProps> = ({
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

export default Section
