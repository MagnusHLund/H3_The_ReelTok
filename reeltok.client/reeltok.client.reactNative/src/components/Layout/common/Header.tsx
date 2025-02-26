import React from 'react'
import Divider from './Divider'
import BackButton from './BackButton'
import { StyleSheet, Text } from 'react-native'
interface HeaderProps {
  title: string
  showBackButton?: boolean
}

const Header: React.FC<HeaderProps> = ({ title, showBackButton }) => {
  return (
    <>
      {showBackButton && <BackButton />}
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
