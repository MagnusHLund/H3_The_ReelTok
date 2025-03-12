import { Modal, View, Text, StyleSheet } from 'react-native'
import CustomButton from '../../input/CustomButton'

interface DialogBoxProps {
  text: string
  visible: boolean
  handleModalClose: () => void
  handleConfirmation: () => void
}

const DialogBox: React.FC<DialogBoxProps> = ({
  text,
  visible,
  handleModalClose,
  handleConfirmation,
}) => {
  return (
    <Modal visible={visible} onRequestClose={handleModalClose} transparent animationType="fade">
      <View style={styles.overlay}>
        <View style={styles.container}>
          <Text style={styles.text}>{text}</Text>
          <View style={styles.buttonContainer}>
            <CustomButton title="No" onPress={handleModalClose} />
            <CustomButton title="Yes" onPress={handleConfirmation} />
          </View>
        </View>
      </View>
    </Modal>
  )
}

const styles = StyleSheet.create({
  overlay: {
    flex: 1,
    backgroundColor: 'rgba(0, 0, 0, 0.5)',
    justifyContent: 'center',
    alignItems: 'center',
  },
  container: {
    width: '80%',
    backgroundColor: 'white',
    padding: 20,
    borderRadius: 10,
    alignItems: 'center',
    shadowColor: '#000',
    shadowOffset: {
      width: 0,
      height: 2,
    },
    shadowOpacity: 0.25,
    shadowRadius: 3.84,
    elevation: 5,
  },
  text: {
    fontSize: 18,
    marginBottom: 20,
    textAlign: 'center',
  },
  buttonContainer: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    width: '100%',
  },
})

export default DialogBox
