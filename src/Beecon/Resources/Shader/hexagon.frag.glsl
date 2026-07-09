#version vigilance_300

in vec2 fragTexCoord;
out vec4 finalColor;

uniform vec2 uResolution;
uniform vec2 uScroll;
uniform float uHexSize;
uniform float uLineThickness;
uniform vec4 uBackgroundColor;
uniform vec4 uLineColor;

const vec2 kHexRepeat = vec2(1.0, 1.7320508);

float hexDistance(vec2 p)
{
    p = abs(p);
    return max(dot(p, normalize(vec2(1.0, 1.7320508))), p.x);
}

vec2 hexCenterOffset(vec2 uv)
{
    vec2 half_ = kHexRepeat * 0.5;
    vec2 a = mod(uv, kHexRepeat) - half_;
    vec2 b = mod(uv - half_, kHexRepeat) - half_;
    return dot(a, a) < dot(b, b) ? a : b;
}

void main()
{
    vec2 world = fragTexCoord * uResolution + uScroll;
    vec2 uv = world / uHexSize;

    vec2 local = hexCenterOffset(uv);
    float distToEdge = 0.5 - hexDistance(local);

    float lineHalf = (uLineThickness * 0.5) / uHexSize;
    float aa = fwidth(distToEdge);
    float line = 1.0 - smoothstep(lineHalf - aa, lineHalf + aa, distToEdge);

    finalColor = mix(uBackgroundColor, uLineColor, line);
}
