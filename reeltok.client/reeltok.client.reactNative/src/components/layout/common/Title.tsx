import { Text, View, StyleSheet } from 'react-native'
import React, { ReactNode } from 'react'
interface TitleProps {
  title?: string
  children: ReactNode
}

const Title: React.FC<TitleProps> = ({ title = '', children }) => {
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

export default Title
