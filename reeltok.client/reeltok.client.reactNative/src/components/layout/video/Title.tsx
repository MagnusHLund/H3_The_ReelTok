import { Text } from 'react-native'
import React from 'react'

interface TitleProps {
  title: string
}

const Title: React.FC<TitleProps> = ({ title }) => {
  return <Text>{title ? 'Ingen title' : ''}</Text>
}

export default Title
