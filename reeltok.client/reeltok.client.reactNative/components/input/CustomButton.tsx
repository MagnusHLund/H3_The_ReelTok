// The button needs functionality to have no borders, nor background color (transparent), to allow for an image inside it.
// It also needs to allow children inside it, like a user, which is clickable (like on the subscribing/subscribers screens).
// Otherwise, its just a standard button that follows an app standard design.
import { useFonts } from "expo-font";
import { TouchableOpacity, StyleSheet, Text } from 'react-native';
import { Poppins_400Regular } from "@expo-google-fonts/poppins";
import { ReactNode } from "react";

interface ButtonProps {
  title?: string,
  children?: ReactNode,
  transparent?: boolean,
  flexDirection?: 'row' | 'column',
  borders?: boolean,
  onPress: () => void,
}


const CustomButton: React.FC<ButtonProps> = ({ children, title, transparent, borders, flexDirection, onPress }) => {
  const [fontsLoaded] = useFonts({
    Poppins_400Regular,
  });
  const buttonStyling = StyleSheet.create({
    button: {
      borderWidth: borders ? 2 : 0,
      backgroundColor: transparent ? 'transparent' : '#ff0050',
      borderRadius: 10,
      padding: 5,
      display: 'flex',
      justifyContent: 'center',
      alignItems: 'center',
      flexDirection: flexDirection,
    },
    buttonText: {
      fontSize: 20,
      fontFamily: 'Poppins_400Regular',
      color: transparent ? 'black' : 'white',
    }
  });

  return(
      <TouchableOpacity style={buttonStyling.button} activeOpacity={1} onPress={onPress}>
        { children }
        {title ? <Text style={buttonStyling.buttonText}>{title}</Text> : null}
      </TouchableOpacity>
  ); 
};

export default CustomButton
