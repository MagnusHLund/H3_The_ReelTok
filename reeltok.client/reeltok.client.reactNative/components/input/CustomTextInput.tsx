// The text input needs to be able to keep its content invisible, unless in focus (for email in settings).
// It also needs to hide the password, while logging in.

interface CustomTextInputProps {
    password?: boolean;
    email?: boolean;
    placeholder: string;

}



import React from 'react';
import { TextInput, StyleSheet } from 'react-native';

const CustomTextInput: React.FC<CustomTextInputProps> = ({ password, email, placeholder }) => {
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

const styles = StyleSheet.create({
    input: {
        height: 40,
        width: 300,
        borderColor: 'gray',
        borderWidth: 1,
        paddingLeft: 10,
        borderRadius: 10,
    },
});

export default CustomTextInput;
