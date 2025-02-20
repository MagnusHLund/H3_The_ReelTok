// The text input needs to be able to keep its content invisible, unless in focus (for email in settings).
// It also needs to hide the password, while logging in.

interface CustomTextInputProps {
    password?: boolean;
    email?: boolean;
    placeholder: string;
    borders?: boolean;
    widthProcentage?: number

}

import React from 'react';
import { TextInput, StyleSheet, useWindowDimensions } from 'react-native';


const CustomTextInput: React.FC<CustomTextInputProps> = ({ password, email, placeholder, borders, widthProcentage=0.8 }) => {
    const {width} = useWindowDimensions();
    const styles = StyleSheet.create({
        input: {
            height: 40,
            width: width * widthProcentage,
            borderColor: 'gray',
            borderWidth: borders ? 1 : 0,
            paddingLeft: 10,
            borderRadius: 10,
            color:'white',
            
            backgroundColor:'#565656'
        },
    });
    return (
        <>
            {password && !email ? (
                <TextInput
                    style={styles.input}
                    placeholder={placeholder}
                    secureTextEntry={password}
                    keyboardType='default'
                />
            ) : email && !password ? (
                <TextInput
                    style={styles.input}
                    placeholder={placeholder}
                    keyboardType='email-address'
                />
            ) : (
                <TextInput
                    style={styles.input}
                    placeholder={placeholder}
                    secureTextEntry={password}
                    keyboardType={email ? 'email-address' : 'default'}
                />
            )}
        </>
    );
};


export default CustomTextInput;
