import { Image, ImageSourcePropType } from 'react-native'
import React from 'react'

interface CustomImageProps {
  resizeMode?: 'cover' | 'contain' | 'stretch' | 'repeat' | 'center'
  source: ImageSourcePropType
  height: number
  width: number
  borderRadius: number
}

const CustomImage: React.FC<CustomImageProps> = ({ resizeMode = 'cover', height,width,borderRadius,source }) => {
  return <Image style={{ resizeMode:resizeMode, height: height, width:width, borderRadius:borderRadius }} source={source} />
}

export default CustomImage
