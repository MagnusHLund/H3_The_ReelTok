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
  widthPercentage?: number | 'auto'
  flexDirection?: 'row' | 'column'
  borders?: boolean
  onPress: () => void
}

const CustomButton: React.FC<ButtonProps> = ({
  children,
  title,
  transparent,
  widthPercentage = 0.33,
  flexDirection,
  borders,
  onPress,
}) => {
  useFonts({
    Poppins_400Regular,
  })
  const { width } = useWindowDimensions()
  const calculatedWidth = widthPercentage === 'auto' ? 'auto' : width * widthPercentage

  return (
    <TouchableOpacity
      style={[
        buttonStyling.button,
        {
          borderWidth: borders ? 2 : 0,
          backgroundColor: transparent ? 'transparent' : '#ff0050',
          width: calculatedWidth,
          flexDirection: flexDirection,
        },
      ]}
      activeOpacity={1}
      onPress={onPress}
    >
      {children}
      {title ? (
        <Text style={[buttonStyling.buttonText, { color: transparent ? 'black' : 'white' }]}>
          {title}
        </Text>
      ) : null}
    </TouchableOpacity>
  )
}

const buttonStyling = StyleSheet.create({
  button: {
    borderRadius: 10,
    padding: 5,
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    gap: 3,
  },
  buttonText: {
    fontSize: 20,
    fontFamily: 'Poppins_400Regular',
    textAlign: 'center',
  },
})

export default CustomButton
