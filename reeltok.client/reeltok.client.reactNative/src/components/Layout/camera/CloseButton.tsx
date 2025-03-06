import { View, StyleSheet } from 'react-native'
import CustomButton from '../../input/CustomButton'
import { Entypo } from '@expo/vector-icons'
import React, { useState } from 'react'
import DialogBox from '../common/DialogBox'
import useTranslation from '../../../hooks/useTranslations'

interface CloseButtonProps {
  onClose: () => void
}

const CloseButton: React.FC<CloseButtonProps> = ({ onClose }) => {
  const [dialogVisible, setDialogVisible] = useState(false)
  const t = useTranslation()

  const handleShowDialog = () => {
    setDialogVisible(true)
  }

  const handleHideDialog = () => {
    setDialogVisible(false)
  }

  const handleConfirmation = () => {
    handleHideDialog()
    onClose()
    setDialogVisible(false)
  }

  return (
    <View style={styles.closeButton}>
      <CustomButton onPress={handleShowDialog} transparent widthPercentage={0.15}>
        <Entypo name="cross" size={50} color="white" />
      </CustomButton>
      {dialogVisible && (
        <DialogBox
          text="Are you sure you want to close the camera?"
          visible={dialogVisible}
          handleModalClose={handleHideDialog}
          handleConfirmation={handleConfirmation}
        />
      )}
    </View>
  )
}

const styles = StyleSheet.create({
  closeButton: {
    width: '10%',
    height: '10%',
    position: 'absolute',
    left: '80%',
    top: '8%',
    zIndex: 1,
  },
})

export default CloseButton
