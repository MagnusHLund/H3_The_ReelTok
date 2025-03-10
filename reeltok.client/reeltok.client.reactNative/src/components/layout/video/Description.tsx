import { Text } from 'react-native'
import React from 'react'

interface DescriptionProps {
  description: string
}

const Description: React.FC<DescriptionProps> = ({ description }) => {
  return <Text>{description ? 'Ingen beskrivelse' : ''}</Text>
}

export default Description
