// The button needs functionality to have no borders, nor background color (transparent), to allow for an image inside it.
// It also needs to allow children inside it, like a user, which is clickable (like on the subscribing/subscribers screens).
// Otherwise, its just a standard button that follows an app standard design.
import { useFonts } from 'expo-font'
import { TouchableOpacity, StyleSheet, Text, useWindowDimensions } from 'react-native'
import { Poppins_400Regular } from '@expo-google-fonts/poppins'
import { ReactNode } from 'react'

interface ButtonProps {
  title?: string
  children?: ReactNode
  transparent?: boolean
  widthPercentage?: number
  flexDirection?: 'row' | 'column'
  borders?: boolean
  justifyPosition?: 'center' | 'flex-start' | 'flex-end' | 'space-between' | 'space-around'
  onPress: () => void
}

const CustomButton: React.FC<ButtonProps> = ({
  children,
  title,
  transparent,
  widthPercentage = 0.33,
  flexDirection,
  borders,
  justifyPosition = 'center',
  onPress,
}) => {
  useFonts({
    Poppins_400Regular,
  })
  const { width } = useWindowDimensions()
  const buttonStyling = StyleSheet.create({
    button: {
      borderWidth: borders ? 2 : 0,
      backgroundColor: transparent ? 'transparent' : '#ff0050',
      borderRadius: 10,
      padding: 5,
      display: 'flex',
      justifyContent: justifyPosition,
      alignItems: 'center',
      gap: 3,
      width: width * widthPercentage,
      flexDirection: flexDirection,
    },
    buttonText: {
      fontSize: 20,
      fontFamily: 'Poppins_400Regular',
      color: transparent ? 'black' : 'white',
      textAlign: 'center',
    },
  })

  return (
    <TouchableOpacity style={buttonStyling.button} activeOpacity={1} onPress={onPress}>
      {children}
      {title ? <Text style={buttonStyling.buttonText}>{title}</Text> : null}
    </TouchableOpacity>
  )
}

export default CustomButton
