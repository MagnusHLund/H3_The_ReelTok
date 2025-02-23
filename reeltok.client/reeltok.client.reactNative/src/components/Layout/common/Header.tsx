import React from 'react'
import { StyleSheet, Text } from 'react-native'
import Divider from './Divider'

interface HeaderProps {
  title: string
}

const Header: React.FC<HeaderProps> = ({ title }) => {
  return (
    <>
      <Text style={styles.title}>{title}</Text>
      <Divider />
    </>
  )
}

const styles = StyleSheet.create({
  title: {
    textAlign: 'center',
    marginTop: 50,
    marginBottom: 20,
    fontWeight: '500',
    fontSize: 30,
  },
})

export default Header
