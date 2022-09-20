
#include "raylib.h"

#define BUILD_LIBTYPE_SHARED
#define RRES_IMPLEMENTATION
#define RRES_RAYLIB_IMPLEMENTATION
#define RRES_SUPPORT_COMPRESSION_LZ4
#define RRES_SUPPORT_ENCRYPTION_AES
#define RRES_SUPPORT_ENCRYPTION_XCHACHA20

#include "rres.h"
#include "rres-raylib.h"