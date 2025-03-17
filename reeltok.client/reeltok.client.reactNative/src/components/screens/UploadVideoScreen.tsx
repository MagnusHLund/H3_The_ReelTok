import React from 'react'
import useAppSelector from '../../hooks/useAppSelector'
import UploadVideo, { isImage } from '../layout/common/UploadVideo'
import Header from '../layout/common/Header'

const UploadVideoScreen = () => {
  const uploadedVideo = useAppSelector((state) => state.upload.video)
  const title = isImage(uploadedVideo.fileUri) ? 'Upload Image' : 'Upload Video'

  return (
    <>
      <Header showBackButton title={title} />
      <UploadVideo />
    </>
  )
}

export default UploadVideoScreen
